# ProfesiNet: A Professional Networking Platform


ProfesiNet is a web application designed to facilitate professional networking and career development. Users can create profiles, share their experiences through posts, and interact with others in the community.

## Technologies

**Back-end:**

* **C#**: The primary language for building the application's core logic and ensuring efficiency.
* **ASP.NET Core**: Provides a robust framework for creating web applications and APIs.
* **Entity Framework Core**: Simplifies database interactions and management.
* **Modular Monolith Architecture**: Divides the application into independent modules for better organization and maintainability.
* **Clean Architecture**: Enforces separation of concerns and promotes code reusability.

**Front-end:**

* **TypeScript**: Adds static typing to JavaScript for enhanced code reliability and maintainability.
* **React**: Enables building dynamic and interactive user interfaces with reusable components.

## Functionalities

* **User Management:**
    * Registration and login system.
    * Profile creation and editing, including:
        * Profile picture.
        * Basic information (name, surname, title, address).
        * Bio.
        * Education history.
        * Work experience.
        * Skills.
* **Networking:**
    * Follow other users to stay updated on their activities.
    * See a curated feed of posts from your network.
* **Content Sharing:**
    * Create and publish posts with text and images.
    * Like and share posts from other users.
    * Comment on posts and engage in discussions.

## Architecture

ProfesiNet adopts a modular monolith architecture, dividing the application into distinct modules:

* **Users Module:** Handles user management, profile information, and authentication. 
* **Posts Module:**  Manages post creation, interactions (likes, shares, comments), and the post feed.
* **Shared Module:** Provides common infrastructure components like message broker and error handling.

Each module adheres to clean architecture principles, ensuring a clear separation of concerns between the domain layer, application layer, and infrastructure layer. This promotes code testability and maintainability.

## Future Development

* Enhance the user interface for a more engaging experience.
* Implement private messaging functionality.
* Develop an admin panel for managing users and content.
* Explore additional features based on user feedback.

## Getting Started

1. Clone the repository: `git clone https://github.com/your-username/profesiNet.git`
2. Install the required dependencies: `npm install`
3. Configure the application settings.
4. Run the application: `npm start`

## Contributing

Contributions are welcome! Please refer to the [contribution guidelines](CONTRIBUTING.md) for more information.

## License

This project is licensed under the [MIT License](LICENSE).
