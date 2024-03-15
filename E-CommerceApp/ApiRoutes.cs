namespace E_CommerceApp;

public static class ApiRoutes
{
    public const string BaseRoute = "api/[controller]";

    public static class Product
    {
        public const string GetProductsByCategoryName = "GetProductsByCategoryName";
        public const string Get = "Get";
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
        public const string CreateComment = "CreateComment";
        public const string DeleteComment = "DeleteComment";
        public const string CreateRate = "CreateRate";
        public const string DeleteRate = "DeleteRate";
    }

    public static class Category
    {
        public const string GetAll = "GetAll";
        public const string Get = "Get";
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }

    public static class Identity
    {
        public const string Register = "Register";
        public const string Login = "Login";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }

    public static class Order
    {
        public const string GetOrdersByUserId = "GetOrdersByUserId";
        public const string Create = "Create";
        public const string AddOrderItem = "AddOrderItem";
        public const string UpdateQuantity = "UpdateQuantity";
    }

    public static class ShoppingSession
    {
        public const string GetCartItem = "GetCartItem";
        public const string GetShoppingSessionItems = "GetShoppingSessionItems";
        public const string Create = "Create";
        public const string UpdateQuantity = "UpdateQuantity";
        public const string DeleteItem = "DeleteItem";
        public const string AddItem = "AddItem";
        public const string CalculateShoppingSessionPrice = "CalculateShoppingSessionPrice";
    }

    public static class UserPayment
    {
        public const string GetByPaymentId = "GetByPaymentId";
        public const string GetAllByUserId = "GetAllByUserId";
        public const string Create = "Create";
        public const string Delete = "Delete";
    }

    public static class Role
    {
        public const string GetAllByName = "GetAllByName";
        public const string GetById = "GetById";
        public const string GetAll = "GetAll";
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";
    }
}