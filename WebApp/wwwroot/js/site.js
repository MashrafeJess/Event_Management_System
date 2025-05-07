// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    let cartCount = 0;

    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll(".add-to-cart").forEach(button => {
            button.addEventListener("click", function (e) {
                e.preventDefault();

                cartCount++;
                updateCartCount(cartCount);
            });
        });

        function updateCartCount(count) {
            const navbarCount = document.getElementById("navbar-cart-count");
            if (navbarCount) {
                navbarCount.textContent = count;

                // Add bounce animation
                navbarCount.classList.remove("animate-bounce");
                void navbarCount.offsetWidth;
                navbarCount.classList.add("animate-bounce");
            }
        }
    });
</script>
