
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------


namespace GS_STB
{

using System;
    using System.Collections.Generic;
    
public partial class FAS_ShiftsDescription
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public FAS_ShiftsDescription()
    {

        this.FAS_ShiftsCounter = new HashSet<FAS_ShiftsCounter>();

    }


    public byte ShiftID { get; set; }

    public string ShiftName { get; set; }

    public string Description { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<FAS_ShiftsCounter> FAS_ShiftsCounter { get; set; }

}

}