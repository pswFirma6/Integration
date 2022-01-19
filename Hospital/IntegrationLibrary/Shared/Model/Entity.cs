using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IntegrationLibrary.Shared.Model
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
