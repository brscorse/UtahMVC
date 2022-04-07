using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UtahMVC.Models;

namespace UtahMVC.Controllers
{
    public class InferenceController : Controller
    {
        private InferenceSession _session;
      

        public InferenceController(InferenceSession session)
        {
            _session = session;
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Score(MLCrash data)
        {
            var result = _session.Run(new List<NamedOnnxValue>
            {
                NamedOnnxValue.CreateFromTensor("float_input", data.AsTensor())
            });
            Tensor<float> score = result.First().AsTensor<float>();
            var prediction = new Prediction { PredictedValue = score.First() };
            
            result.Dispose();
            ViewBag.Score = Math.Round(prediction.PredictedValue);
            return View("Analytics");
        }
    }
}
