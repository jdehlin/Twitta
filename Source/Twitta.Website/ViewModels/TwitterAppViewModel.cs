using System.ComponentModel.DataAnnotations;

namespace Twitta.Website.ViewModels
{
    public class TwitterAppViewModel
    {
        public virtual int ApiApplicationId { get; set; }
        [Required, MaxLength(100)]
        public virtual string AppName { get; set; }
        [Required, MaxLength(100)]
        public virtual string ConsumerKey { get; set; }
        [Required, MaxLength(100)]
        public virtual string ConsumerKeySecret { get; set; }
        [MaxLength(100)]
        public virtual string Token { get; set; }
        [MaxLength(100)]
        public virtual string TokenSecret { get; set; }
    }
}