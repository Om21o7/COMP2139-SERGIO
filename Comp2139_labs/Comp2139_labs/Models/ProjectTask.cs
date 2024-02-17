using System;
using System.ComponentModel.DataAnnotations;

namespace Comp2139_labs.Models
{
	public class ProjectTask
	{
		[Key]
		public int ProjectTaskId { get; set; }

		public string? Title { get; set; }

		public string? Description { get; set; }

		// foreiiegn key

		public int ProjectId { get; set; }

		// Navigation property
		// This allows for easy access ti rekated Project antity from them ProjectsTask entity

		public Project? Project { get; set; }
	}
}

