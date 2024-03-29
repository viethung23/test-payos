using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace NetCoreDemo.Controllers;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Net.payOS;
public class CheckoutController : Controller
{
    private readonly PayOS _payOS;
    private readonly ILogger<CheckoutController> _logger;


    public CheckoutController(PayOS payOS,ILogger<CheckoutController> logger)
    {
        _payOS = payOS;
        _logger = logger;

    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        _logger.LogInformation("hehehehe hehehe hehehehe heheheheh");
        return View("index");
    }
    [HttpGet("/cancel")]
    public IActionResult Cancel()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        return View("cancel");
    }
    [HttpGet("/success")]
    public IActionResult Success()
    {
        // Trả về trang HTML có tên "MyView.cshtml"
        _logger.LogInformation("nhảy vào link /Success");
        return View("success");
    }
    [HttpPost("/create-payment-link")]
    public async Task<IActionResult> Checkout()
    {
        try
        {
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            List<ItemData> items = new ();
            PaymentData paymentData = new PaymentData(orderCode, 5000, "Thanh toan don hang", items, "https://localhost:3002/cancel", "https://localhost:3002/success");

            CreatePaymentResult createPayment = await _payOS.createPaymentLink(paymentData);

            return Redirect(createPayment.checkoutUrl);
        }
        catch (System.Exception exception)
        {
            Console.WriteLine(exception);
            return Redirect("https://localhost:3002/");
        }
    }
}
