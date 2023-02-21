
using System.ComponentModel.DataAnnotations;

namespace hitsLab.presentation.dto 
{
    public class PageInfoDto
    {
        [Range(0, int.MaxValue)]
        public int Size {get; set; }
        [Range(0, int.MaxValue)]
        public int Count {get; set; }
        [Range(0, int.MaxValue)]
        public int Current {get; set; }

        public PageInfoDto(int size, int count, int current) 
        {
            Size = size;
            Count = count;
            Current = current;
        }
    }
}