using System.Runtime.Remoting.Messaging;
using System.Windows.Data;

namespace AllProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        [Required]
        [StringLength(20)]
        public string Phone { get; set; }

        public int? OrderID { get; set; }
        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        public Order Order { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

    }
}