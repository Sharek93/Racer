//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Racer
{
    using System;
    using System.Collections.Generic;
    
    public partial class Map
    {
        public int ID { get; set; }
        public string start { get; set; }
        public string koniec { get; set; }
        public byte[] mapa { get; set; }
        public int punkty_kontrolne { get; set; }
        public byte[] mini_mapa { get; set; }
    }
}
