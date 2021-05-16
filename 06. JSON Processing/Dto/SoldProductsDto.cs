using System;
using System.Collections.Generic;
using System.Text;

namespace ProductShop.Dto
{
    public class SoldProductsDto
    {
        public int Count { get; set; }

        public ProductDetailsDto[] Products { get; set; }
    }
}
