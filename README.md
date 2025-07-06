🧪 Core Functional Modules
1. Procurement Management
•	Business users create procurement plans per retail shop
•	Items are selected from a vendor’s catalog
•	Plans are submitted for execution
2. Vendor Catalog
•	Vendors register and manage products by category
•	Business users browse only approved vendor catalogs
•	SKUs and prices are pre-defined by vendors
3. Contracts & Shipments
•	Contracts are signed with vendors upon plan approval
•	Shipment info is logged (including dispatch/delivery status)
4. Inventory & Returns
•	Received items update shop-level inventory
•	Business users can raise returns for damaged goods
•	Stock is tracked per retail location
5. User Management
•	Business users (Procurement Executives, Managers) login
•	Role-based permissions using JWT + ASP.NET Identity
•	Users tied to specific retail shops
Interview-Worthy Highlights
•	✅ Designed for separation of concerns: services are autonomous and loosely coupled
•	✅ Uses Dapper for performance and control over SQL
•	✅ Identity is centralized and secured via JWT
•	✅ Catalog is vendor-owned, eliminating negotiation complexity
•	✅ Retail shops are fully isolated units (multi-tenant ready)
•	✅ Architecture is scalable and cloud-deployable
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Microservices Architecture
🔹 1. ProcurementService
Handles procurement plans and catalog lookup.
Responsibilities
Create/view Procurement Plans
Manage PlanItems
List vendor catalogs (VendorProduct, Category)
List plans per shop/vendor
Fetch plan details

🔹 2. ContractService
Manages vendor contracts and shipments (Phase 2).
Responsibilities
Create contracts from accepted plans
Manage contract terms
Create/view shipments
Track shipments by plan
________________________________________
🔹 3. InventoryService
Handles receiving, stock tracking, and returns (Phase 3).
Responsibilities
Update inventory after shipment
Insert and track return orders
Maintain stock per shop
Generate inventory views/reports
________________________________________
🔹 4. VendorService
Handles vendor registration and catalog updates.
Responsibilities
Manage VendorProfile
Add/update VendorProduct, VendorCategory
View vendor ratings
Authentication/authorization (optional)
________________________________________
🔹 5. IdentityService (Recommended standalone)
Use ASP.NET Identity here to manage JWT, users, roles, and claims.
Responsibilities
Register/Login APIs
Issue/validate JWT
Link business users to RetailShop
Role-based access (ProcurementExec, Manager, Vendor)
________________________________________
⚙️ Technologies for Each Microservice
Concern	Technology
API Hosting	.NET 8 ASP.NET Core Web API
DB Access	Dapper + Stored Procedures
Security	ASP.NET Identity + JWT Bearer
Auth DB	Shared or per-service Identity DB
Inter-Service Comm	REST now, eventing later (RabbitMQ / Azure Service Bus)
Logging	Serilog / ELK
Monitoring	HealthChecks, Prometheus (optional)
