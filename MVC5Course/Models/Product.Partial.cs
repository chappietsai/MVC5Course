using System.Web.Mvc;
namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {

        public int 訂單數量
        {
            get {

                return this.OrderLine.Count; //會取出所有值 才開始計算 故 效能不好

             ///Will 有較好的寫法

            }
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
        [DisplayFormat(DataFormatString =("{0:0}"),ApplyFormatInEditMode =true)]
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
