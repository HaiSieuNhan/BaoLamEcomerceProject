function updateCart(CartDetailId, Quantity,event) {
    $.ajax({
        type: 'put',
        dataType: "json",
        async: false,
        url: "/Cart/UpdateCart",
        data: {
            CartDetailId: CartDetailId,
            Quantity: Quantity
        },
        success: function (res) {
            var cart = JSON.parse(res.cart);           
            if (res.isValid == true) {
                $(".total #NotionalPrice ").html(cart.NotionalPrice + "đ")
                $(".total #TotalPrice ").html(cart.TotalPrice + "đ")               
                $(".CartCount ").html("Giỏ hàng ["+ res.cartCount+"]" )               
            } else {
                if (res.quantity != 0) {
                    $(".total #NotionalPrice ").html(cart.NotionalPrice + "đ")
                    $(".total #TotalPrice ").html(cart.TotalPrice + "đ")
                    $(".CartCount ").html("Giỏ hàng [" + res.cartCount + "]")   
                    $(event).val(res.quantity);
                }
                $("#bar-error").empty();
                $("#bar-error").append('<div class="update-cart-error">' + res.msg + '</div >');
                $("#bar-error").fadeIn();
                $("#bar-error").delay(3000).fadeOut();
                
            }
            
        },
        error: function (err) {
            console.log(err);
        }
    })
}
function deleteCart(CartDetailId, event) {
    $.ajax({
        type: 'delete',
        dataType: "json",
        async: false,
        url: "/Cart/DeleteCart",
        data: {
            CartDetailId: CartDetailId,
        },
        success: function (res) {
            var cart = JSON.parse(res.cart);
            if (res.isValid == true) {            
                $(".total #NotionalPrice ").html(cart.NotionalPrice + "đ")
                $(".total #TotalPrice ").html(cart.TotalPrice + "đ")
                if (cart.DiscountPrice == 0) {
                    $("#discount-price ").remove();
                } else {
                    $("#discount-price span:last-child").html(cart.DiscountPrice + 'đ');
                }
                
                $(".CartCount ").html("Giỏ hàng [" + res.cartCount + "]")
                $(event).parent().parent().parent().remove();
                if (res.cartCount == 0) {
                    $(".colorlib-product").remove();
                    var html = '<div class="empty"><p class="empty-note" > Không có sản phẩm nào trong giỏ hàng của bạn.</p><a href="/" class="btn btn-primary">Tiếp tục mua sắm</a></div>'
                    $(".breadcrumbs").after(html)
                }
            } else {
                var html = '<div class="message"><p class="message-inner"><svg stroke="currentColor" fill="currentColor" stroke-width="0" viewBox="0 0 24 24" size="20" class="message-icon" height="20" width="20" xmlns="http://www.w3.org/2000/svg"><path d="M14.59 8L12 10.59 9.41 8 8 9.41 10.59 12 8 14.59 9.41 16 12 13.41 14.59 16 16 14.59 13.41 12 16 9.41 14.59 8zM12 2C6.47 2 2 6.47 2 12s4.47 10 10 10 10-4.47 10-10S17.53 2 12 2zm0 18c-4.41 0-8-3.59-8-8s3.59-8 8-8 8 3.59 8 8-3.59 8-8 8z"></path></svg><span class="message-text">' + res.msg + '</span></p ></div > '
                $(".order-progress").after(html);
                $('html, body').animate({
                    scrollTop: $('html').offset().top
                })
            }

        },
        error: function (err) {
            console.log(err);
        }
    })
}
function applyCoupon(form) {
    $.ajax({
        type: 'post',
        dataType: "json",
        async: false,
        url: form.action,
        data: new FormData(form),
        contentType: false,
        processData: false,
        success: function (res) {
            var cart = JSON.parse(res.cart);
            if (res.isValid == true) {
                $(".total #NotionalPrice ").html(cart.NotionalPrice + "đ")
                $(".total #TotalPrice ").html(cart.TotalPrice + "đ")
                if ($("#discount-price").length == 0) {
                    $(".sub").append('<p id="discount-price"><span> Giảm giá:</span ><span>' + cart.DiscountPrice + 'đ</span></p>')
                } else {
                    $("#discount-price span:last-child").html(cart.DiscountPrice + 'đ');
                }
            } else {

                $("#bar-error").empty();
                $("#bar-error").append('<div class="update-cart-error">' + res.msg + '</div >');
                $("#bar-error").fadeIn();
                $("#bar-error").delay(3000).fadeOut();
            }
            

        },
        error: function (err) {
            console.log(err);
        }
    })
    return false;
}
function reply(event) {
    var data = $(event).attr("data-attr");
    $('.reply-input[data-attr=' + data + ']').toggle();
}
function replycancel(event) {
    var data = $(event).attr("data-attr");

    $('.reply-input[data-attr=' + data + ']').hide();
}
function exit() {
    $('#snack-bar').hide();
}