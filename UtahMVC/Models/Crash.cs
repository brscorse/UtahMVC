using System;
using System.ComponentModel.DataAnnotations;

namespace UtahMVC.Models
{
    public class Crash
    {
        [Key]
        [Required]
        public string CRASH_ID { get; set; }

        [Required(ErrorMessage = "Please add the route: ")]
        public string ROUTE { get; set; }

        [Required(ErrorMessage = "Please add the milepoint: ")]
        public string MILEPOINT { get; set; }

        [Required(ErrorMessage = "Please add the latitude: ")]
        public string LAT_UTM_Y { get; set; }

        [Required(ErrorMessage = "Please add the longitude: ")]
        public string LONG_UTM_X { get; set; }

        [Required(ErrorMessage = "Please add the address/road name: ")]
        public string MAIN_ROAD_NAME { get; set; }

        [Required(ErrorMessage = "Please add the city: ")]
        public string CITY { get; set; }

        [Required(ErrorMessage = "Please add the county: ")]
        public string COUNTY_NAME { get; set; }

        [Required(ErrorMessage = "Please add the severity: ")]
        public string CRASH_SEVERITY_ID { get; set; }

        [Required]
        public bool WORK_ZONE_RELATED { get; set; }

        [Required]
        public bool PEDESTRIAN_INVOLVED { get; set; }

        [Required]
        public bool BICYCLIST_INVOLVED { get; set; }

        [Required]
        public bool MOTORCYCLE_INVOLVED { get; set; }

        [Required]
        public bool IMPROPER_RESTRAINT { get; set; }

        [Required]
        public bool UNRESTRAINED { get; set; }

        [Required]
        public bool DUI { get; set; }

        [Required]
        public bool INTERSECTION_RELATED { get; set; }

        [Required]
        public bool WILD_ANIMAL_RELATED { get; set; }

        [Required]
        public bool DOMESTIC_ANIMAL_RELATED { get; set; }

        [Required]
        public bool OVERTURN_ROLLOVER { get; set; }

        [Required]
        public bool COMMERCIAL_MOTOR_VEH_INVOLVED { get; set; }

        [Required]
        public bool TEENAGE_DRIVER_INVOLVED { get; set; }

        [Required]
        public bool OLDER_DRIVER_INVOLVED { get; set; }

        [Required]
        public bool NIGHT_DARK_CONDITION { get; set; }

        [Required]
        public bool SINGLE_VEHICLE { get; set; }

        [Required]
        public bool DISTRACTED_DRIVING { get; set; }

        [Required]
        public bool DROWSY_DRIVING { get; set; }

        [Required]
        public bool ROADWAY_DEPARTURE { get; set; }

        [Required(ErrorMessage = "Please enter the year: ")]
        public string CRASH_YEAR { get; set; }

        [Required(ErrorMessage = "Please enter the month: ")]
        public string CRASH_MONTH { get; set; }

        [Required(ErrorMessage = "Please enter the day: ")]
        public string CRASH_DAY { get; set; }

        [Required(ErrorMessage = "Please enter the hour: ")]
        public string CRASH_HOUR { get; set; }


    }
}