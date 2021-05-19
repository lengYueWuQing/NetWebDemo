using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace WebApplication.Models
{
    public class Person: IValidatableObject
    {
        [JsonPropertyName("userName"), ModelBinder(Name = "userName")]
        [Required(ErrorMessage = "名称不能为空")]
        public string name { get; set; }
        [Range(1, 200, ErrorMessage = "年龄设置无效")]
        public int age { get; set; }
        [JsonPropertyName("password"), ModelBinder(Name = "password")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "密码长度有误"),Required(AllowEmptyStrings =false, ErrorMessage ="密码不能为空")]
        public string passWord { get; set; }

        [JsonProperty(ItemConverterType = typeof(MyDateTimeConverter))]
        [Required(ErrorMessage ="时间不能为null")]
        public DateTime now { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (passWord.StartsWith("Test"))
            {
                yield return new ValidationResult(
                    "密码不能以Test 开头" 
                    , new[] { nameof(age) }
                );
            }
        }
    }

    public class MyDateTimeConverter: IsoDateTimeConverter
    {
        MyDateTimeConverter() {
            DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
