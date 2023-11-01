﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML.Transforms;
using Microsoft.ML;

namespace HousingIntel
{
    public partial class HouseIntelML
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"MSZoning", @"MSZoning"),new InputOutputColumnPair(@"LotConfig", @"LotConfig"),new InputOutputColumnPair(@"BldgType", @"BldgType")}, outputKind: OneHotEncodingEstimator.OutputKind.Indicator)      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(new []{new InputOutputColumnPair(@"MSSubClass", @"MSSubClass"),new InputOutputColumnPair(@"LotArea", @"LotArea"),new InputOutputColumnPair(@"OverallCond", @"OverallCond"),new InputOutputColumnPair(@"YearBuilt", @"YearBuilt"),new InputOutputColumnPair(@"YearRemodAdd", @"YearRemodAdd"),new InputOutputColumnPair(@"BsmtFinSF2", @"BsmtFinSF2"),new InputOutputColumnPair(@"TotalBsmtSF", @"TotalBsmtSF")}))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"Exterior1st",outputColumnName:@"Exterior1st"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"MSZoning",@"LotConfig",@"BldgType",@"MSSubClass",@"LotArea",@"OverallCond",@"YearBuilt",@"YearRemodAdd",@"BsmtFinSF2",@"TotalBsmtSF",@"Exterior1st"}))      
                                    .Append(mlContext.Transforms.NormalizeMinMax(@"Features", @"Features"))      
                                    .Append(mlContext.Regression.Trainers.FastTreeTweedie(new FastTreeTweedieTrainer.Options(){NumberOfLeaves=4,MinimumExampleCountPerLeaf=17,NumberOfTrees=17,MaximumBinCountPerFeature=57,FeatureFraction=0.902971084466096,LearningRate=0.999999776672986,LabelColumnName=@"SalePrice",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
