// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$('.button-container input[type="file"]').change(function (event) {
    $('.picture-preview').remove();
    $('p.error').remove();
    if (this.files) {
        var filesAmount = this.files.length;
        if (filesAmount > 5) {
            $(this).val('');
            $('<p class="error">Chỉ được chọn tối đa 5 hình</p>').insertAfter('.picture-input');
        } else {
            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();
                reader.onload = function (event) {
                    if ($('.picture-preview').length) {
                    } else {
                        $('<div class="picture-preview"></div>').insertAfter('.picture-input');
                    }
                    $('.picture-preview').append('<div class="picture"></div>');
                    $($.parseHTML('<img>')).attr('src', event.target.result).appendTo('.picture:last-child');

                }
                reader.readAsDataURL(this.files[i]);
            }
        }
    }
});


$(document).ready(function () {

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
    $('.reply-input').hide();
    $('.button').click(function () {
        $('.button-container input[type="file"]').trigger('click');
    });
    $('.flnOER').click(function () {
        $('.ReactModalPortal').hide();
    });
    var cw = $('.product-entry .productimg a.prod-img').width();
    $('.product-entry .productimg a.prod-img').css({ 'height': cw + 'px' });

});
$("#slider-carousel").owlCarousel({
    autoplay: true,
    slideSpeed: 2000,
    autoplayTimeout: 5000,
    dots: true,
    pagination: false,
    loop: true,
    items: 1,
    itemsDesktop: [1199, 4],
    itemsDesktopSmall: [980, 3],
    itemsTablet: [768, 2],
    itemsMobile: [479, 1]
});
$("#hot-carousel").owlCarousel({
    autoplay: false,
    smartSpeed: 2000,
    autoplayTimeout: 5000,
    autoplaySpeed: 2000,
    dots: false,
    pagination: false,
    touchDrag: false,
    mouseDrag: false,
    nav: true,
    loop: true,
    items: 4,
    navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
    itemsDesktop: [1199, 4],
    itemsDesktopSmall: [980, 3],
    itemsTablet: [768, 2],
    itemsMobile: [479, 1]
});
$("#collection-carousel").owlCarousel({
    autoplay: true,
    smartSpeed: 2000,
    autoplayTimeout: 5000,
    autoplaySpeed: 2000,
    dots: false,
    pagination: false,
    nav: true,
    loop: true,
    items: 4,
    navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
    itemsDesktop: [1199, 4],
    itemsDesktopSmall: [980, 3],
    itemsTablet: [768, 2],
    itemsMobile: [479, 1]
});
$("#logo-carousel").owlCarousel({
    autoplay: true,
    slideSpeed: 2000,
    autoplayTimeout: 4000,
    dots: false,
    pagination: false,
    loop: true,
    items: 7,
    itemsDesktop: [1199, 4],
    itemsDesktopSmall: [980, 3],
    itemsTablet: [768, 2],
    itemsMobile: [479, 1]
});

    $(".product-entry .product-image-color .color").hover(function (event) {
        var proAttributeId = $(this).attr('id');
    $(this).parent().parent().find('li a').removeClass('active');
    $(this).addClass('active');
    var productHTML = $(this).parent().parent().parent();
        $.ajax({
        type: 'Get',
    dataType: "json",
    async: false,
    url: "http://localhost:50776/Home/GetProductHomepageByProductAttribute",
        data: {
        productAttributeId: proAttributeId,
    },
            success: function (res) {
                var productHomepage = JSON.parse(res.productHomepage);
    productHTML.find('.productimg .prod-img img').attr("src", "/images/" + productHomepage.UrlImage);
    productHTML.find('.desc .price del span').html(productHomepage.Price + '₫');
    productHTML.find('.desc .price ins span.price-amount').html(productHomepage.PriceSale + '₫');
    productHTML.find('.desc .price ins span.percent').html('-' + productHomepage.PercentSale + '%');
    var url = "/ProductDetail/GetProductDetail?ProductAttributeId=" + productHomepage.Id;
    productHTML.find('.productimg .prod-img').attr('href', url);
    productHTML.find('.desc .prod-img').attr('href', url);
},

        error: function (err) {
        console.log(err);
}
})

    })
 





