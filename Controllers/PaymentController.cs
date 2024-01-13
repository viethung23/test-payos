namespace NetCoreDemo.Controllers;

using Microsoft.AspNetCore.Mvc;
using NetCoreDemo.Types;
using Net.payOS;
using Net.payOS.Types;
[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PayOS _payOS;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(PayOS payOS,ILogger<PaymentController> logger)
    {
        _payOS = payOS;
        _logger = logger;
    }

    [HttpPost("payos_transfer_handler")]
    public IActionResult payOSTransferHandler(WebhookType body)
    {
        _logger.LogInformation("Đã vào hàm ròi -- 1");
        _logger.LogInformation(body.ToString());
        try
        {
            WebhookData data = _payOS.verifyPaymentWebhookData(body);
            _logger.LogInformation($"đax gọi hàm VerifyData {data.amount}, {data.orderCode},{data.orderCode}");

            if (data.description == "Ma giao dich thu nghiem" || data.description == "VQRIO123")
            {
                return Ok(new Response(0, "Ok", null));
            }
            return Ok(new Response(0, "Ok", null));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return Ok(new Response(-1, "fail", null));
        }

    }
}