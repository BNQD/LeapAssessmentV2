
-----------------

User --> Load Balancer --> Auto Scaling Group (EC2) --> Application
Application --> Handling Purchases - Queue Service --> SQL Server
Application --> Redis --> SQL Server

```mermaid
	A[User] --> B[Load Balancer]
	B --> C[Auto Scaling Group (EC2)]
	C --> D[Application]
	D --> User Purchased --> F[SQL Server]
	D --> Shopping Cart Info --> E[Redis]

```

------------------
Load Balancer - Distributes requests across EC2 instances
Auto Scaling Group - Automatically scales based on traffic
Application - Fetches ticket availability, handles purchases by placing order in queue service, writes shopping cart data to redis
Redis - Cache Aside - To store ticket availability
	- Write Through - For shopping cart data
Queue Service - To ensure purchases are handled, no need for caching since this data is not readily needed
SQL Server - Stores all data (Users, purchases)
** When tickets are purchased, redis should fetch updated ticket availability from SQL Server
-------------------
