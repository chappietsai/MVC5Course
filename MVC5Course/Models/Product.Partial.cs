using System.Web.Mvc;
using System.Linq;
namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {

        public int 訂單數量
        {
            get
            {

                //return this.OrderLine.Count; //會取出所有值 才開始計算 故 效能不好

                ///Will 有較好的寫法
                //return this.OrderLine.Count;
                //return this.OrderLine.Where(p => p.Qty > 400).Count;
                //return this.OrderLine.Where(p => p.Qty > 400).ToList().Count;
                             //return this.OrderLine.Count(p => p.Qty > 400);
                             return this.OrderLine.Count();
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 100 && this.Stock > 5)
            {
                yield return new ValidationResult("價格與庫存數量不合理",
                    new string[] { "Price", "Stock" });
            }

            if (this.OrderLine.Count() > 5 && this.Stock == 0)
            {
                yield return new ValidationResult("Stock 與訂單數量不匹配",
                    new string[] { "Stock" });
            }

            yield break;

        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        [Required(ErrorMessage = "請輸入商品名稱")]
        //[MinLength(3), MaxLength(30)]
        // [RegularExpression("(.+)-(.+)", ErrorMessage = "商品名稱格式錯誤")]
        [DisplayName("商品名稱")]
        [必須包含will字串(ErrorMessage = "商品名稱必須包含Will字串")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品價格範圍")]
        [DisplayFormat(DataFormatString = ("{0:0}"), ApplyFormatInEditMode = true)]
        [DisplayName("商品價錢")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "請設定正確的商品庫存數量")]
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}