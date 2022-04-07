using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UtahMVC.Models
{
    public class MLCrash
    {

        public float City_Salt_Lake_City { get; set; }

        public float County_Name_Utah { get; set; }

        public float Pedestrian_Involved { get; set; }

        public float Bicyclist_Involved { get; set; }

        public float Motorcycle_Involved { get; set; }

        public float Unrestrained_True { get; set; }

        public float DUI_True { get; set; }

        public float Overturn_Rollover_True { get; set; }

        public float Commercial_Vehicle_Involved_True { get; set; }

        public float Distracted_Driving { get; set; }


        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
                City_Salt_Lake_City, County_Name_Utah, Pedestrian_Involved, Bicyclist_Involved, Motorcycle_Involved,
                Unrestrained_True, DUI_True, Overturn_Rollover_True, Commercial_Vehicle_Involved_True, Distracted_Driving
            };
            int[] dimensions = new int[] { 1, 10 };
            return new DenseTensor<float>(data, dimensions);
        }
    }
}
