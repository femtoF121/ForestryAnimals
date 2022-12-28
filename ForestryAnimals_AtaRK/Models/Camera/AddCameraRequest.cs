﻿using System.ComponentModel.DataAnnotations;

namespace ForestryAnimals_AtaRK.Models.Camera
{
    public class AddCameraRequest
    {
        [Required] public int ForestryId { get; set; }
        [Required] public int SerialNumber { get; set; }
    }
}
