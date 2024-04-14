# :books: Comic Online Store - ASP.NET Core Application
## :memo: Project Overview
The Comic Online Store is an application designed for users to explore and purchase various comics across different categories.
<br /> 
Users can browse through comics, add them to their shopping cart, remove items from the cart as needed, and proceed with their purchases at their convenience. Additionally, the application offers a section dedicated to providing brief biographies and information about the creators behind the comics.

**Admin users hold additional privileges within the application**, including:

* Management of comics, creators, and categories, including adding, editing, and deleting functionalities.
* Granting admin rights to other registered users, facilitated through the admin area.

## :boy: Seeded Admin User
To streamline testing the admin area, the application seeds an admin user in the database upon initialization.
<br /> 
**Login credentials:**
* Email: admin@mail.com
* Password: 123456a

## :pushpin: Application Architecture
The Comic Online Store follows a structured architecture, adhering to the MVC (Model-View-Controller) pattern. Controllers are responsible for loading data into models or view models and forwarding them to corresponding services. These services, in turn, manage interactions with the database.

## :wrench: Technologies Used
The application is built using the following technologies:

* ASP.NET Core 6.0
* Entity Framework Core 6.0
* Bootstrap
