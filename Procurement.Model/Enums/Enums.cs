using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procurement.Model.Enums
{
    public enum UserType
    {
        Admin = 1,
        Custumer = 2,
        Supplier = 3,
        Manager = 4
    }

    public enum UserLegalPersonality
    {
        PhysicalPerson = 1,
        JuridicalPerson = 2
    }


    public enum AttributeType
    {
        None = 0,
        /// <summary>
        /// Dropdown list
        /// </summary>
        DropdownList = 1,
        /// <summary>
        /// Radio list
        /// </summary>
        RadioList = 2,
        /// <summary>
        /// Checkboxes
        /// </summary>        
        Checkboxes = 3,
        /// <summary>
        /// TextBox
        /// </summary>
        TextBox = 4,
        /// <summary>
        /// Multiline textbox
        /// </summary>
        MultilineTextbox = 10,
        /// <summary>
        /// Datepicker
        /// </summary>
        Datepicker = 20,
        ///// <summary>
        ///// File upload control
        ///// </summary>
        //FileUpload = 30,
        ///// <summary>
        ///// Color squares
        ///// </summary>
        //ColorSquares = 40,
        ///// <summary>
        ///// Read-only checkboxes
        ///// </summary>
        //ReadonlyCheckboxes = 50,
    }



}
