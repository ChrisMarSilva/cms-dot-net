
-- ----------------------------------------------------
-- ----------------------------------------------------

-- rabbitmq
-- docker run -d --hostname rabbitmq --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3.11-management

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_product_api;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM PRODUCT;

-- DELETE FROM PRODUCT;
-- INSERT INTO PRODUCT(id, name, price, description, category_name, image_url) VALUES( 2, 'Camiseta No Internet', 69.9, 'It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.<br/>The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using Content here, content here, making it look like readable English.<br/>Many desktop publishing packages and web page editors now use Lorem Ipsum as their default model text, and a search for lorem ipsum will uncover many web sites still in their infancy.', 'T-shirt', 'https://github.com/leandrocgsi/erudio-microservices-dotnet6/blob/main/ShoppingImages/2_no_internet.jpg?raw=true');

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_identity_server;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM AspNetRoleClaims;
SELECT * FROM AspNetUserLogins;
SELECT * FROM AspNetUserTokens;

SELECT * FROM AspNetRoles;
SELECT * FROM AspNetUsers;
SELECT * FROM AspNetUserClaims;
SELECT * FROM AspNetUserRoles;

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_cart_api;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM cart_header;
-- DELETE FROM cart_header;
SELECT * FROM cart_detail;
-- DELETE FROM cart_detail;
SELECT * FROM product;
-- DELETE FROM product;

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_coupon_api;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM coupon;
-- ERUDIO_2022_10
-- ERUDIO_2022_15

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_order_api;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM order_header;
-- DELETE FROM order_header;
SELECT * FROM order_detail;
-- DELETE FROM order_detail;

-- ----------------------------------------------------
-- ----------------------------------------------------

-- use geek_shopping_email;
-- SET SQL_SAFE_UPDATES = 0;

SELECT * FROM email_logs;
-- DELETE FROM email_logs;

-- ----------------------------------------------------
-- ----------------------------------------------------


















