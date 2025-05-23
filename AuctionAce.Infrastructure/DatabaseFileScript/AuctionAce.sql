USE [AuctionAce]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 02.03.2025 11:43:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[city] [nvarchar](50) NULL,
	[street] [nvarchar](50) NULL,
	[zip_code] [nvarchar](50) NULL,
	[building_number] [int] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auction_Items]    Script Date: 02.03.2025 11:43:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auction_Items](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[id_auctions] [int] NULL,
	[description] [nvarchar](max) NULL,
	[starting_price] [nchar](10) NULL,
	[buy_now_price] [nchar](10) NULL,
	[new_used] [bit] NULL,
	[id_status] [int] NULL,
	[guid] [nvarchar](max) NULL,
	[isBought] [bit] NULL,
 CONSTRAINT [PK_Auction_Items] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[auction_name] [nvarchar](max) NULL,
	[id_users] [int] NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[id_data] [int] NULL,
	[id_category] [int] NULL,
	[description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Auctions] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Auctions_Items_Photos]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions_Items_Photos](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[auction_item_id] [int] NULL,
	[auctions_id] [int] NULL,
	[path] [nvarchar](max) NULL,
	[file_name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Auctions_Items_Photos] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bid_History]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bid_History](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[price] [decimal](18, 0) NULL,
	[isWin] [bit] NULL,
	[id_auction_items] [int] NULL,
	[id_users] [int] NULL,
	[date] [datetime] NULL,
	[user_email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Bid_History] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](max) NULL,
	[descripton] [nvarchar](max) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chat_History]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chat_History](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[auction_item_id] [int] NULL,
	[user_id] [int] NULL,
	[date] [datetime] NULL,
	[message] [nvarchar](max) NULL,
	[user_email] [nvarchar](max) NULL,
 CONSTRAINT [PK_Chat_History] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Leaderboards]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leaderboards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_user] [int] NULL,
	[auction_item_id] [int] NULL,
	[price] [int] NULL,
	[isFinal] [bit] NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_Leaderboards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO [dbo].[Roles] ([name])
VALUES ('user'), ('auctioner')
GO
/****** Object:  Table [dbo].[UserBoughtItems]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserBoughtItems](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_auction_item] [int] NULL,
	[id_user] [int] NULL,
	[prize] [int] NULL,
 CONSTRAINT [PK_UserBoughtItems] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NULL,
	[surname] [nvarchar](50) NULL,
	[email] [nvarchar](50) NULL,
	[password] [nvarchar](50) NULL,
	[id_roles] [int] NULL,
	[id_status] [int] NULL,
	[id_address] [int] NULL,
	[isLogined] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Wishlist]    Script Date: 02.03.2025 11:43:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Wishlist](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_auction] [int] NULL,
	[id_user] [int] NULL,
	[isLiked] [bit] NULL,
 CONSTRAINT [PK_Wishlist] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Auction_Items]  WITH CHECK ADD  CONSTRAINT [FK_Auction_Items_Auctions] FOREIGN KEY([id_auctions])
REFERENCES [dbo].[Auctions] ([id])
GO
ALTER TABLE [dbo].[Auction_Items] CHECK CONSTRAINT [FK_Auction_Items_Auctions]
GO
ALTER TABLE [dbo].[Auctions]  WITH CHECK ADD  CONSTRAINT [FK_Auctions_Category] FOREIGN KEY([id_category])
REFERENCES [dbo].[Category] ([id])
GO
ALTER TABLE [dbo].[Auctions] CHECK CONSTRAINT [FK_Auctions_Category]
GO
ALTER TABLE [dbo].[Auctions]  WITH CHECK ADD  CONSTRAINT [FK_Auctions_Users] FOREIGN KEY([id_users])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Auctions] CHECK CONSTRAINT [FK_Auctions_Users]
GO
ALTER TABLE [dbo].[Auctions_Items_Photos]  WITH CHECK ADD  CONSTRAINT [FK_Auctions_Items_Photos_Auction_Items] FOREIGN KEY([auction_item_id])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[Auctions_Items_Photos] CHECK CONSTRAINT [FK_Auctions_Items_Photos_Auction_Items]
GO
ALTER TABLE [dbo].[Auctions_Items_Photos]  WITH CHECK ADD  CONSTRAINT [FK_Auctions_Items_Photos_Auctions] FOREIGN KEY([auctions_id])
REFERENCES [dbo].[Auctions] ([id])
GO
ALTER TABLE [dbo].[Auctions_Items_Photos] CHECK CONSTRAINT [FK_Auctions_Items_Photos_Auctions]
GO
ALTER TABLE [dbo].[Bid_History]  WITH CHECK ADD  CONSTRAINT [FK_Bid_History_Auction_Items] FOREIGN KEY([id_auction_items])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[Bid_History] CHECK CONSTRAINT [FK_Bid_History_Auction_Items]
GO
ALTER TABLE [dbo].[Bid_History]  WITH CHECK ADD  CONSTRAINT [FK_Bid_History_Users] FOREIGN KEY([id_users])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Bid_History] CHECK CONSTRAINT [FK_Bid_History_Users]
GO
ALTER TABLE [dbo].[Chat_History]  WITH CHECK ADD  CONSTRAINT [FK_Chat_History_Auction_Items] FOREIGN KEY([auction_item_id])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[Chat_History] CHECK CONSTRAINT [FK_Chat_History_Auction_Items]
GO
ALTER TABLE [dbo].[Chat_History]  WITH CHECK ADD  CONSTRAINT [FK_Chat_History_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Chat_History] CHECK CONSTRAINT [FK_Chat_History_Users]
GO
ALTER TABLE [dbo].[Leaderboards]  WITH CHECK ADD  CONSTRAINT [FK_Leaderboards_Auction_Items] FOREIGN KEY([auction_item_id])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[Leaderboards] CHECK CONSTRAINT [FK_Leaderboards_Auction_Items]
GO
ALTER TABLE [dbo].[Leaderboards]  WITH CHECK ADD  CONSTRAINT [FK_Leaderboards_Auction_Items1] FOREIGN KEY([auction_item_id])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[Leaderboards] CHECK CONSTRAINT [FK_Leaderboards_Auction_Items1]
GO
ALTER TABLE [dbo].[Leaderboards]  WITH CHECK ADD  CONSTRAINT [FK_Leaderboards_Users] FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Leaderboards] CHECK CONSTRAINT [FK_Leaderboards_Users]
GO
ALTER TABLE [dbo].[UserBoughtItems]  WITH CHECK ADD  CONSTRAINT [FK_UserBoughtItems_Auction_Items] FOREIGN KEY([id_auction_item])
REFERENCES [dbo].[Auction_Items] ([id])
GO
ALTER TABLE [dbo].[UserBoughtItems] CHECK CONSTRAINT [FK_UserBoughtItems_Auction_Items]
GO
ALTER TABLE [dbo].[UserBoughtItems]  WITH CHECK ADD  CONSTRAINT [FK_UserBoughtItems_Users] FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[UserBoughtItems] CHECK CONSTRAINT [FK_UserBoughtItems_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Address] FOREIGN KEY([id_address])
REFERENCES [dbo].[Address] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Address]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([id_roles])
REFERENCES [dbo].[Roles] ([id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_Auctions] FOREIGN KEY([id_auction])
REFERENCES [dbo].[Auctions] ([id])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_Auctions]
GO
ALTER TABLE [dbo].[Wishlist]  WITH CHECK ADD  CONSTRAINT [FK_Wishlist_Users] FOREIGN KEY([id_user])
REFERENCES [dbo].[Users] ([id])
GO
ALTER TABLE [dbo].[Wishlist] CHECK CONSTRAINT [FK_Wishlist_Users]
GO
