namespace Application.Enums;

public enum ErrorCode
{
    NotFound = 404,
    ServerError = 500,

//Validation errors should be in the range 100 - 199
    ValidationError = 101,
    FriendRequestValidationError = 102,

//Infrastructure errors should be in the range 200-299
    IdentityCreationFailed = 202,
    DatabaseOperationException = 203,

//Application errors should be in the range 300 - 399
// Product errors should be in the range 300-305
    ProductCreationNotPossible = 300,
    ProductNotValid = 301,
    ProductCategoryCreationNotPossible = 302,

    ProductCategoryNotValid = 303,
    ProductCategoryDeletionFailed = 304,
    ProductCommentNotValid = 305,

    // User errors should be in the range 306-310
    UserAlreadyExists = 305,
    UserDoesNotExist = 306,
    IncorrectPassword = 307,

    // ShippingSession errors should be in the range 311-315
    CartItemNotFound = 402,
    CartItemNotValid = 403,
    CartItemDeletionFailed = 404,
    // Orders errors should be in the range 316-320

    OrderDetailsNotValid = 317,
    OrderItemNotValid = 318,
    // Orders errors should be in the range 321-325

    ShoppingSessionNotValidException = 321,

    UnknownError = 999,
}