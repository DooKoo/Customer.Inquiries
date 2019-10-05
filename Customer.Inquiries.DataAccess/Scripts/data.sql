SET IDENTITY_INSERT [dbo].[Customers] ON

INSERT INTO [dbo].[Customers]
           ([CustomerId], [CreatedDate], [CustomerName], [ContactEmail], [MobileNumber])
     VALUES
           (123456, GETDATE(), 'Firstname Lastname', 'user@domain.com', '0123456789'),
     (123457, GETDATE(), 'Firstname1 Lastname1', 'user1@domain.com', '0123456790'),
     (123458, GETDATE(), 'Firstname2 Lastname2', 'user2@domain.com', '0123456791')

SET IDENTITY_INSERT [dbo].[Customers] OFF

INSERT INTO [dbo].[Transactions]
   (TransactionDateTime, CreatedDate, Amount, Currency, CustomerId, Status)
  VALUES
   (GETDATE(), GETDATE(), 1234.56, 'USD', 123457, 0),
   (GETDATE(), GETDATE(), 1234.56, 'USD',123458, 0),
   (GETDATE(), GETDATE(), 0.56, 'THB', 123458, 1)