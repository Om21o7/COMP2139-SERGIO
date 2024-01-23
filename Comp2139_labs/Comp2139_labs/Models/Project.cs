using System;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Comp2139_labs.Models
{
	public class Project
	{
		[Required]

		public int ProjectId { get; set; }

		public required string Name { get; set; }

		public string? Description { get; set; }

        [DataType(DataType.Date)]

		public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]

        public DateTime EndDate { get; set; }

		public string? Status { get; set; }

    }
}

