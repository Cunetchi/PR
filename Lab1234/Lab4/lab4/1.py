import requests
from requests.packages.urllib3.exceptions import InsecureRequestWarning

# Disable insecure request warnings
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

base_url = "https://localhost:5001/api/Category"

def list_categories():
    try:
        response = requests.get(f"{base_url}/categories",verify=False)
        response.raise_for_status()  # Raise exception for 4XX or 5XX status codes
        categories = response.json()
        for category in categories:
            print(category)
        print("Categories listed successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error listing categories: {e}")

def create_category():
    try:
        name = input("Enter category name: ")
        payload = {"name": name}
        response = requests.post(f"{base_url}/categories", json=payload, verify=False)
        response.raise_for_status()
        print(response.json())
        print("Category created successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error creating category: {e}")


def get_category_by_id():
    try:
        id = input("Enter category ID: ")
        response = requests.get(f"{base_url}/categories/{id}",verify=False)
        response.raise_for_status()
        print(response.json())
        print("Category retrieved successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error retrieving category: {e}")

def delete_category_by_id():
    try:
        id = input("Enter category ID to delete: ")
        response = requests.delete(f"{base_url}/categories/{id}",verify=False)
        response.raise_for_status()
        print(response.json())
        print("Category deleted successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error deleting category: {e}")

def list_products_by_category_id():
    try:
        id = input("Enter category ID to list products: ")
        response = requests.get(f"{base_url}/categories/{id}/products",verify=False)
        response.raise_for_status()
        products = response.json()
        for product in products:
            print(product)
        print("Products listed successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error listing products: {e}")

def search_categories():
    try:
        query = input("Enter search query: ")
        response = requests.get(f"{base_url}/categories/search?q={query}",verify=False)
        response.raise_for_status()
        categories = response.json()
        for category in categories:
            print(category)
        print("Categories searched successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error searching categories: {e}")

def update_category_by_id():
    try:
        id = input("Enter category ID to update: ")
        name = input("Enter new category name: ")
        payload = {"name": name}
        response = requests.put(f"{base_url}/{id}", json=payload,verify=False)
        response.raise_for_status()
        print(response.json())
        print("Category updated successfully.")
    except requests.exceptions.RequestException as e:
        print(f"Error updating category: {e}")

# Main menu
while True:
    print("\n1. List categories")
    print("2. Create category")
    print("3. Get category by ID")
    print("4. Delete category by ID")
    print("5. List products by category ID")
    print("6. Search categories")
    print("7. Update category by ID")
    print("8. Exit")
    choice = input("Enter your choice: ")

    if choice == "1":
        list_categories()
    elif choice == "2":
        create_category()
    elif choice == "3":
        get_category_by_id()
    elif choice == "4":
        delete_category_by_id()
    elif choice == "5":
        list_products_by_category_id()
    elif choice == "6":
        search_categories()
    elif choice == "7":
        update_category_by_id()
    elif choice == "8":
        break
    else:
        print("Invalid choice. Please try again.")
