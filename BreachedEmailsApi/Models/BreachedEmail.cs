using System;
using System.ComponentModel.DataAnnotations;

namespace BreachedEmailsApi.Models
{
    [Serializable]
    public class BreachedEmail
    {
        [Key]
        public string Email { get; set; }
    }
}
