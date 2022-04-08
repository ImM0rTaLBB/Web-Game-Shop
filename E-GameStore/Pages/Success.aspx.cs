using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

public partial class Pages_Success : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<Cart> carts = (List<Cart>)Session[User.Identity.GetUserId()];

        Cartmodel model = new Cartmodel();
        model.MarkOrderAsPaid(carts);

        Session[User.Identity.GetUserId()] = null;
    }
}