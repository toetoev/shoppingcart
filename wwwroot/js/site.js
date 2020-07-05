// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code
$(document).ready(function () {
    count();

    function count() {
        var countString = localStorage.getItem("myproduct");

        if (countString) {
            var countarray = JSON.parse(countString);
            var total = 0;
            $.each(countarray, function (i, v) {
                var qty = v.qty;
                total += qty;    
            })
            $("#checkouttext").html(total);
        }
    }

    showcomment();
    function showcomment() {
        var liststring = localStorage.getItem("commentform");
        //console.log(liststring);

        if (liststring) {
            var commentarray = JSON.parse(liststring);
            var html = '';

            $.each(commentarray, function (i, v) {

                var id = v.id;
                var comment = v.comment;

                html += `<tr>
                             <td>${comment}</td>
                         </tr>`;
            })

            $("#commentList").html(html);
        }
        else {
            $("#commentList").html('');
        }
    }


    
    $(".addtocart").click(function () {
        var id = $(this).data('id');
        var name = $(this).data('name');
        var description = $(this).data('description');
        var price = $(this).data('price');
        var image = $(this).data('image');

        var cart = {
            id: id,
            name: name,
            description: description,
            price: price,
            image: image,
            qty:1
        }

        var producestring = localStorage.getItem("myproduct");
        var changearray;

        if (producestring == null) {
            changearray = Array();
        }
        else {
            changearray = JSON.parse(producestring);
        }
        var status = false;

        $.each(changearray, function (i, v) {
            if (id == v.id) {
                status = true;
                v.qty++;
            }
        })

        if (!status) {
            changearray.push(cart);
        }

        var productlist = JSON.stringify(
            changearray);
        localStorage.setItem("myproduct", productlist);
        count();
    })


    var addtoCart = function (url) {
        $("#cartButton").load(url);
    };

    showtable();
    function showtable() {
        var liststring = localStorage.getItem("myproduct");
        //console.log(liststring);

        if (liststring) {
            var productarray = JSON.parse(liststring);
            var html = '';
            var amt = 0;
            var total = 0;
            var j = 1;
            var str = "";

            $.each(productarray, function (i, v) {
                var id = v.id;
                var qty = v.qty;
                str += id + ',' + qty + ' ';
            })
            //console.log(str);

            $.each(productarray, function (i, v) {

                var id = v.id;
                var name = v.name;
                var image = v.image;
                var price = v.price;
                var qty = v.qty;
                var description = v.description;
                var subtotal = price * qty;
                var x = parseInt(subtotal * 100) / 100;
                total += x;
                amt = Math.ceil(total *100)/100;

                html += `<tr>
                             <td><input type="hidden" value="${str}" name="productlist"></td>
                         </tr>
                          <tr>                            
					        <td>${j++}</td>
                            <td>
                                <img src ="${image}" width="80" height="50">
                            </td>
					        <td id="product">${name}</td>
					        <td id="price">${price}</td>
					        <td>
                               <div class="input-group mb-3">
                                    <div class="input-group-prepend">
                                        <button class="add" data-id=${i}> <i class="fa fa-plus-square"></i></button>
                                    </div>
                                    <input type="text" class="form-control" style="flex:unset; width:25%;" value="${qty}" id="qty" name="quantity">
                                    <div class="input-group-append">
                                        <button class="sub" data-id=${i}><i class="fa fa-minus-square"></i></button>
                                    </div>
                                </div>
					        </td>	
					        <td>${x}</td>
                            <td>
                                <button class="deleteitem" data-id=${i}><i class="fa fa-trash" aria-hidden="true"></i></button>
                            </td>
					       </tr>`;
            })

            $("#productlist").html(html);
            $("#table_total").html(amt);
        }
        else {
            $("#productlist").html('');
            $("#table_total").html('');
        }
    }

    $("#productlist").on('click', '.add', function () {
        var id = $(this).data('id');
        var liststring = localStorage.getItem("myproduct");
        //console.log(id, liststring);

        if (liststring) {
            var productarray = JSON.parse(liststring);

            $.each(productarray, function (i, v) {
                if (id == i) {
                    var qty = v.qty;
                    qty++;
               
                    productarray[id].qty = qty;
                }
            })
            
            localStorage.setItem('myproduct', JSON.stringify(productarray));
            count();
            showtable();
            
        }
    })

    $("#productlist").on('click', '.sub', function () {
        var id = $(this).data('id');
        var liststring = localStorage.getItem("myproduct");

        if (liststring) {
            var productarray = JSON.parse(liststring);

            $.each(productarray, function (i, v) {
                if (id == i) {
                    var qty = v.qty;
                    qty--;

                    productarray[id].qty = qty;
                }

                if (qty == 0) {
                    productarray.splice(id, 1);
                }
            })

            localStorage.setItem('myproduct', JSON.stringify(productarray));
            count();
            showtable();
        }
    })

    $("#productlist").on('click', '.deleteitem', function () {
        var id = $(this).data('id');
        deleteItem(id);
        count();
    })

    function deleteItem(id) {

        var mycart = localStorage.getItem("myproduct");
        console.log(mycart);

        if (mycart) {
            var cartarray = JSON.parse(mycart);
            cartarray.splice(id, 1);
            console.log(cartarray);
            localStorage.setItem('myproduct', JSON.stringify(cartarray));
            showtable();
        }
    }

    $('#checkoutfun').click(function () {
        localStorage.clear();
        count();
    });

    paypal.Buttons({
        createOrder: function (data, actions) {
            // This function sets up the details of the transaction, including the amount and line item details.
            return actions.order.create({
                purchase_units: [{
                    amount: {
                        value: '0.01' // pass the order total value
                    }
                }]
            });
        },
        onApprove: function (data, actions) {
            // This function captures the funds from the transaction.
            return actions.order.capture().then(function (details) {
                // This function shows a transaction success message to your buyer.
                $('#exampleModalCenter').modal('hide');
                alert("payment successfully");
                $("#orderform").submit();
                localStorage.clear();
                count();
            });
        }
    }).render('#paypal-button-container');
})

