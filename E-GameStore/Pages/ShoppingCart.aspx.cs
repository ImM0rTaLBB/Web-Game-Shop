using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;


public partial class Pages_ShoppingCart : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string userId = User.Identity.GetUserId();
        GetPurchasesInCart(userId);
    }

    private void GetPurchasesInCart(string userId)
    {
        Cartmodel model = new Cartmodel();
        double subTotal = 0;

        List<Cart> purchaseList = model.GetOrderInCart(userId);
        CreateShopTable(purchaseList, out subTotal);

        double vat = subTotal * 0.12;
        double totalAmount = subTotal + vat + 15;

        litTotal.Text = "$ " + subTotal;
        litVat.Text = "$" + vat;
        litTotalAmount.Text = "$ " + totalAmount;
    }

    private void CreateShopTable(List<Cart> purchaseList, out double subTotal)
    {
        subTotal = new Double();
        ProductModel model = new ProductModel();

        foreach(Cart cart in purchaseList)
        {
            Product product = model.GetProduct(cart.ProductID);

            ImageButton btnImage = new ImageButton
            {
                ImageUrl = string.Format("~/Images/Products/{0}", product.Image),
                PostBackUrl = string.Format("~/Pages/Product.aspx?id={0}", product.ID)
            };

            LinkButton lnkDelete = new LinkButton()
            {
                PostBackUrl = string.Format("~/Pages/ShoppingCart.aspx?productId={0}", cart.ID),
                Text = "Delete Item",
                ID = "del" + cart.ID
            };

            lnkDelete.Click += Delete_Item;

            //Fill amount list with numbers 1-20
            int[] amount = Enumerable.Range(1, 20).ToArray();
            DropDownList ddlAmount = new DropDownList
            {
                DataSource = amount,
                AppendDataBoundItems = true,
                AutoPostBack = true,
                ID = cart.ID.ToString()
            };
            ddlAmount.DataBind();
            ddlAmount.SelectedValue = cart.Amount.ToString();
            ddlAmount.SelectedIndexChanged += ddlAmount_SelectedIndexChanged;

            //Create table to hold shopping cart details
            Table table = new Table { CssClass = "CartTable" };
            TableRow row1 = new TableRow();
            TableRow row2 = new TableRow();

            TableCell cell1_1 = new TableCell { RowSpan = 2, Width = 30 };
            TableCell cell1_2 = new TableCell
            {
                Text = string.Format("<h4>{0}</h4><br />{1}<br/>In Stock",
                product.Name, "Item No:" + product.ID),
                HorizontalAlign = HorizontalAlign.Left,
                Width = 350,
            };
            TableCell cell1_3 = new TableCell { Text = "Unit Price<hr/>" };
            TableCell cell1_4 = new TableCell { Text = "Quantity<hr/>" };
            TableCell cell1_5 = new TableCell { Text = "Item Total<hr/>" };
            TableCell cell1_6 = new TableCell();

            TableCell cell2_1 = new TableCell();
            TableCell cell2_2 = new TableCell { Text = "£ " + product.Price };
            TableCell cell2_3 = new TableCell();
            TableCell cell2_4 = new TableCell { Text = "£ " + Math.Round((cart.Amount * (double)product.Price), 2) };
            TableCell cell2_5 = new TableCell();



            //Set custom controls
            cell1_1.Controls.Add(btnImage);
            cell1_6.Controls.Add(lnkDelete);
            cell2_3.Controls.Add(ddlAmount);

            //Add rows & cells to table
            row1.Cells.Add(cell1_1);
            row1.Cells.Add(cell1_2);
            row1.Cells.Add(cell1_3);
            row1.Cells.Add(cell1_4);
            row1.Cells.Add(cell1_5);
            row1.Cells.Add(cell1_6);

            row2.Cells.Add(cell2_1);
            row2.Cells.Add(cell2_2);
            row2.Cells.Add(cell2_3);
            row2.Cells.Add(cell2_4);
            row2.Cells.Add(cell2_5);
            table.Rows.Add(row1);
            table.Rows.Add(row2);
            pnlShoppingCart.Controls.Add(table);

            //Add total of current purchased item to subtotal
            subTotal += (cart.Amount * (double)product.Price);
        }

        //Add selected objects to Session
        Session[User.Identity.GetUserId()] = purchaseList;
    }

    private void ddlAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList selectedList = (DropDownList)sender;
        int quantity = Convert.ToInt32(selectedList.SelectedValue);
        int cartId = Convert.ToInt32(selectedList.ID);

        Cartmodel model = new Cartmodel();
        model.UpdateQuantity(cartId, quantity);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private void Delete_Item(object sender, EventArgs e)
    {
        LinkButton selectedLink = (LinkButton)sender;
        string link = selectedLink.ID.Replace("del", "");
        int cartId = Convert.ToInt32(link);

        Cartmodel model = new Cartmodel();
        model.DeleteCart(cartId);

        Response.Redirect("~/Pages/ShoppingCart.aspx");
    }

    private string GeneratePaypalButton(double subTotal)
    {
        //Set Paypal parameters
        string paypal = string.Format(
            @"<script async='async' src='https://www.paypalobjects.com/js/external/paypal-button.min.js?merchant=garageseller@gmail.com' 
                data-button='buynow' 
                data-name='Garage Purchases' 
                data-quantity=1
                data-amount='{0}' 
                data-tax='{1}'
                data-shipping='15'
                data-callback='http://localhost:50992/Pages/Success.aspx'
                data-sendback='http://localhost:50992/Pages/Success.aspx'
                data-env='sandbox'>
             </script>", subTotal, (subTotal * 0.21));

        return paypal;
    }

}
