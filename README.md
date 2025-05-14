# Concycle Backend - ASP.NET Core API

Concycle is a digital platform that facilitates the exchange of knowledge, skills, and assistance requests among its users in exchange for contribution points. The objective is to cultivate a culture of mutual support within the community, thereby enabling individuals to access personal development experiences and everyday assistance — without the need for monetary resources.

## 🔗 Backend API (ASP.NET Core + PostgreSQL)

### Architecture:
- `Concycle.Core`: Entity and DTO definitions
- `Concycle.Data`: EF Core + Repositories
- `Concycle.Business`: Services layer
- `Concycle.API`: RESTful Controllers

### Main Entities:

#### 🧍 User
- Starts with 100 score
- Cannot apply to own posts
- Score only changes through transactions

#### 📦 Post
- `type`: `"skill"` (earn score), `"help"` (spend score)
- `category`: user-defined

#### 📄 ConRequest
- Users apply to posts
- Status: `"Pending"`, `"Accepted"`, `"Rejected"`
- When help is marked complete, a transaction is triggered

#### 💸 Transaction
- Transfers score between users
- Direction depends on post type

## ⚙️ Flow

1. User registers and gets 100 score
2. Creates a post (help or skill)
3. Other users apply
4. Post owner accepts the request
5. After help is completed, "Mark as Completed" is clicked
6. Transaction is created and score is transferred

## 🚀 How to Run

1. Open `Concycle.sln` with Visual Studio
2. PostgreSQL is used as the database (configure connection string in `appsettings.json`)
3. Run the API at `http://localhost:5047`

## 👨‍💻 Developer

[LinkedIn: Furkan Yıldız](https://www.linkedin.com/in/furkan-yıldız-584383254)
