import requests

# (warning)Pylance(intelsense) nu poate vedea importul la InsecureRequestWarning (tot ok, skill issue la intelsense)
# Suppress SSL warnings
from requests.packages.urllib3.exceptions import InsecureRequestWarning
requests.packages.urllib3.disable_warnings(InsecureRequestWarning)

# la realizarea requesturilor verify va fi pus pe false
# verify e pus pe false deoarece serverul e pe localhost, 
# iar certificatul ssl nu este trusted (self asigned)
# nu se recomanda folosirea in programe reale (production)
class UtmShopClient:
    def __init__(self):
        self.api_url = "https://localhost:5001/api/Category"

    def get_categories(self):
        return requests.get(self.api_url + "/categories", verify=False).json()

    def create_category(self, model:dict):
        return requests.post(self.api_url + "/categories", json=model, verify=False)

    def get_category(self, id:int):
        return requests.get(self.api_url + f"/categories/{id}", verify=False).json()

    def delete_category(self, id:int):
        return requests.delete(self.api_url + f"/categories/{id}", verify=False)

    def get_products_from_category(self, id:int):
        return requests.get(self.api_url + f"/categories/{id}/products", verify=False).json()

    def create_product_in_category(self, id:int, model:dict):
        return requests.post(self.api_url + f"/categories/{id}/products", json=model, verify=False).json()

    def search_category(self, categoryName:str):
        return requests.get(self.api_url + "/categories/search", params={"categoryName":categoryName}, verify=False).json()

    def update_category(self, id:int, model:dict):
        return requests.put(self.api_url.split("/categories")[0] + f"/{id}", json=model, verify=False).json()

def testing_all_endpoints():
    client = UtmShopClient()
    nice_response = lambda response : response if response == "<Response [200]>" else f"{response} - Yep, go see docs for endpoints (probably already something exists/doesn't/deleted)"
    print("\nAll categories:\n", client.get_categories())
    id = 4
    print(f"\nCategory details {id=}\n", client.get_category(id))
    model = { "title": "New Category3" }
    print(f"\nCreate new category {model}:\n", nice_response(client.create_category(model)))
    print(f"\nDeleting category {id=}\n", nice_response(client.delete_category(id)))
    model = {
        "id": 5, # category id //
        "title": "string",
        "price": 0,
        "categoryId": 0
    }
    id = model["id"]
    print(f"\nCreate product {id=} -> {model=}\n", client.create_product_in_category(id, model))
    print(f"\nList products from category {id=}\n", client.get_products_from_category(id))
    model = { "title": "VERY IMPORTANT TITTLE" }
    id = 5
    print(f"\nUpdate category {id=} -> {model=}\n", client.update_category(id, model))
    categoryName = model["title"]
    print(f"\nSearch for {categoryName=}\n id is ->", client.search_category(categoryName))

if __name__ == "__main__":
    testing_all_endpoints()