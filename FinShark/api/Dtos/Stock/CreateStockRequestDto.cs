using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Stock
{
    public class CreateStockRequestDto
    {
        //Data val for symbol
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string Symbol { get; set; } = string.Empty; //If we don't initialize as an empty string we'll get null reference errors, don't want to store null when we have an empty string, we just would want an empty string
        
        //Also identify by company name and data val
        [Required]
        [MaxLength(10, ErrorMessage = "CompanyName cannot be over 10 characters")]
        public string CompanyName { get; set; } = string.Empty; //Makes Data Type be nvarchar(MAX)

        //In order to customize a data type in the DB you need to set it right before you make the getter and setter for the column name
        //So then, the data type for Purchase would be decimal(18,2) max 18 digits, and has 2 decimal places
        //Gonna deal with money, so we need to make sure we have monetary amount for money
        //Purchase
        [Required]
        [Range(1, 1000000000000)] //Data val to make sure the purchase price number is in the correct range that would make sense
        public decimal Purchase { get; set; }
        
        //The dividend one
        [Required]
        [Range(0.001, 100)]
        public decimal LastDiv { get; set; }

        //Industry data
        [Required]
        [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
        public string Industry { get; set; } = string.Empty;
        
        //Market Cap data (make long can it be in the trillions)
        [Required]
        [Range(1, 5000000000000)]
        public long MarketCap { get; set; } //Data Type == bigint  
    }
}