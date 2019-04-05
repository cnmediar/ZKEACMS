using System;
using System.Collections.Generic;
using System.Text;

namespace EasyFrameWork.Models
{
   public class VocationalTraining
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public string TrainingBody { get; set; }
        public string Certificate { get; set; }
        public string FilePath { get; set; }
    }
}
