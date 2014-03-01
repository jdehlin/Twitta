
INSERT [dbo].[TwitterApps] ([AppName], [ConsumerKey], [ConsumerKeySecret], [Token], [TokenSecret]) VALUES 
(
N'TestApp', N'xhw1XE9yrah0GqivXvF7gw', 
N'sDEmBEgx2CSwFDP3KBpXaGUqm6TLpM7QWCYHrvBvlZk', 
N'55518774-v6EcOuWqrv1Wb1XUlFQIS6JUtPrBO5BJOXw2eCd5a', 
N'WGfwri6YlxdnQPlXMIRVDt1brXBknAFCcDdyJdU')

INSERT INTO [dbo].[Searches]([Title],[AllOfTheseWords],[ResultType])VALUES('Actavis','actavis',0)
INSERT INTO [dbo].[Searches]([Title],[AllOfTheseWords],[ResultType])VALUES('#mylanhacksummit','#mylanhacksummit',0)
INSERT INTO [dbo].[Searches]([Title],[AllOfTheseWords],[ResultType])VALUES('pharma','pharma',0)
INSERT INTO [dbo].[Searches]([Title],[AllOfTheseWords],[ResultType])VALUES('Mylan','mylan',0)
INSERT INTO [dbo].[Searches]([Title],[AllOfTheseWords],[ResultType])VALUES('iPhone','iphone',0)
GO

