//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesManagementControl.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PurchaseStatus
    {
        public PurchaseStatus()
        {
            this.Purchase = new HashSet<Purchase>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Purchase> Purchase { get; set; }
    }
}
