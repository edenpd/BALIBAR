using System;
using System.Collections.Generic;
using System.Linq;
using BALIBAR.Models;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using Microsoft.ML.Runtime.Api;
using Microsoft.ML.Models;
using Microsoft.ML.Transforms;

namespace BALIBAR.Services
{
    public class RecommendationService
    {
        public PredictionModel<BarData, BarPrediction> Model { get; set; }

        public RecommendationService()
        {
            
        }

        /// <summary>
        /// Trains the model given a collection of reservations
        /// </summary>
        /// <param name="raw_data"></param>
        public void Train(ICollection<Reservation> raw_data, ICollection<BALIBAR.Models.Type> interests)
        {
            // Parses the data 
            var data = new List<BarData>();

            foreach(var curr in raw_data)
            {
                float recommended = interests.Select(g => g.Id).Contains(curr.Bar.Type.Id) ? 1 : 0;
                data.Add(new BarData()
                {
                    MaxParticipants = curr.Bar.MaxParticipants,
                    MinAge = curr.Bar.MinAge,
                    Recommended = recommended
                });
            }
            var collection = CollectionDataSource.Create(data);

            // Creates the learning pipe and add the parsed data
            var pipeline = new LearningPipeline();
            pipeline.Add(collection);
            pipeline.Add(new ColumnConcatenator("Features",
            "MinAge", "MaxParticipants"));

            // Adds a classifier and train the model
            pipeline.Add(new LinearSvmBinaryClassifier() {
                Caching = CachingOptions.Memory,
                NormalizeFeatures = NormalizeOption.Auto
            });

            this.Model = pipeline.Train<BarData, BarPrediction>();
        }

        public IEnumerable<Bar> PredictRecommendedRooms(ICollection<Bar> bars)
        {
            List<Tuple<Bar, float>> bars_score = new List<Tuple<Bar, float>>();

            foreach (var bar in bars)
            {
                var prediction = this.Model.Predict(new BarData()
                {
                    MaxParticipants = bar.MaxParticipants,
                    MinAge = bar.MinAge,
                });

                bars_score.Add(new Tuple<Bar, float>(bar, prediction.PredictedRecommended));
            }

            return (bars_score.OrderBy(x => Math.Abs(x.Item2)).Select(y => y.Item1));
        }
    }


    public class BarData
    {
        [ColumnName("Label")]
        [Column("0")]
        public float Recommended;
        [Column("1")]
        public float MinAge;
        [Column("2")]
        public float MaxParticipants;
    }

    public class BarPrediction
    {
        [ColumnName("Score")]
        public float PredictedRecommended;
    }
}
