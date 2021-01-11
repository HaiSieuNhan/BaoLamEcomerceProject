function paginate(event, pageNumber, productPerPage, cateId) {
    var currentPage = $(".kiFxlu a.active").attr("data-attr");
    var nextPage;
    if ($(event).hasClass("next")) {
        nextPage = Number(currentPage) + 1;
    } else if ($(event).hasClass("prev")) {
        nextPage = Number(currentPage) - 1;
    } else {
        nextPage = $(event).attr("data-attr");
    }
    onclickAttr = $(".btnum[data-attr=" + nextPage + "]").attr('onclick');
    $(".btnum[data-attr=" + currentPage + "]").removeClass("active")
    $(".btnum[data-attr=" + currentPage + "]").attr('onclick', onclickAttr);
    $(".btnum[data-attr=" + nextPage + "]").addClass("active")
    $(".btnum[data-attr=" + nextPage + "]").removeAttr('onclick');
    if (nextPage == 1) {
        $(".kiFxlu .prev").removeAttr('onclick');
    } else if (nextPage == pageNumber) {
        $(".kiFxlu .next").removeAttr('onclick');
    }
    if (currentPage == 1) {
        $(".kiFxlu .prev").attr('onclick', onclickAttr)
    } else if (currentPage == pageNumber) {
        $(".kiFxlu .next").attr('onclick', onclickAttr)
    }

    var categoryId = cateId;
    var skip = productPerPage * (nextPage - 1);
    var take = productPerPage;
    $.ajax({ // Hàm ajax
        type: "get", //Phương thức truyền post hoặc get
        dataType: "json", //Dạng dữ liệu trả về xml, json, script, or html
        async: false,
        url: "/ProductList/GetProductPagination", // Nơi xử lý dữ liệu
        data: {
            CategoryId: categoryId,
            skip: skip,
            take: take,
        },
        beforeSend: function () {
            // Có thể thực hiện công việc load hình ảnh quay quay trước khi đổ dữ liệu ra
        },
        success: function (res) {
            var listProduct = JSON.parse(res.listProduct);
            var listProductLength = Object.keys(listProduct).length;
            $('.item-paginate').first().find('.product-image-color').remove();
            var item = $('.item-paginate').first();
            $('.item-paginate').remove();
            for (var i = 0; i < listProductLength; i++) {
                if (Object.keys(listProduct[i].ProductHomepageAttributeViewModel).length != 0) {
                    var clone = item.clone();
                    clone.find(".productimg a").attr("href", "/ProductDetail/GetProductDetail?ProductAttributeId=" + listProduct[i].ProductHomepageAttributeViewModel[0].Id)
                    clone.find(".productimg a img").attr("src", "/images/" + listProduct[i].ProductHomepageAttributeViewModel[0].UrlImage);
                    clone.find(".productimg a img").attr("alt", listProduct[i].Name);
                    if (Object.keys(listProduct[i].ProductHomepageAttributeViewModel).length > 1) {
                        html = '<ul class=" product-image-color">';
                        for (var j = 0; j < Object.keys(listProduct[i].ProductHomepageAttributeViewModel).length; j++) {
                            html += '<li><a class="prod-img color" href="/ProductDetail/GetProductDetail?ProductAttributeId=' + listProduct[i].ProductHomepageAttributeViewModel[j].Id + '" id="' + listProduct[i].ProductHomepageAttributeViewModel[j].Id + '">';
                            html += '<img src="/images/' + listProduct[i].ProductHomepageAttributeViewModel[j].UrlImage + '" class="img-fluid" alt="' + listProduct[i].Name + '"></a></li>'
                        }
                        html += '</ul>';
                        clone.find(".productimg").after(html);
                    }
                    clone.find(".desc h2 a").attr("href", "/ProductDetail/GetProductDetail?ProductAttributeId=" + listProduct[i].ProductHomepageAttributeViewModel[0].Id)
                    clone.find(".desc h2 a").html(listProduct[i].Name)
                    clone.find(".desc .price del .price-amount").html(listProduct[i].ProductHomepageAttributeViewModel[0].Price + "₫")
                    clone.find(".desc .price ins .price-amount").html(listProduct[i].ProductHomepageAttributeViewModel[0].PriceSale + "₫")
                    clone.find(".desc .price ins .percent").html("-" + listProduct[i].ProductHomepageAttributeViewModel[0].PercentSale + "%")
                    $(".paginate").append(clone);
                }
            }
            $('script[src="/js/site.js"]').remove();
            var reloadJs1 = document.createElement("script");
            reloadJs1.src = '/js/site.js';
            $("body").append(reloadJs1);

        },
        error: function (jqXHR, textStatus, errorThrown) {
            //Làm gì đó khi có lỗi xảy ra
            console.log('The following error occured: ' + textStatus, errorThrown);
        }
    });
}