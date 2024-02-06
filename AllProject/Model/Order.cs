using System.Runtime.Remoting.Messaging;
using System.Windows.Data;

namespace AllProject.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            Clients = new HashSet<Client>();
            OrderCompositions = new HashSet<OrderComposition>();
        }

        public int ID { get; set; }

        public int ClientID { get; set; }

        [Column(TypeName = "date")]
        public DateTime CompilationDate { get; set; }

        public int TotalDuration { get; set; }

        public double OrderPrice { get; set; }

        public bool Issue { get; set; }

        public bool Readiness { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Clients { get; set; }

        public virtual Client Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderComposition> OrderCompositions { get; set; }
    }
}