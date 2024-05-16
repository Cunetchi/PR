import dns.resolver, dns.reversename, dns.exception, ipaddress, subprocess, socket

resolver = dns.resolver.Resolver()
resolver.nameservers = ['8.8.8.8', '2001:4860:4860::8888', '8.8.4.4', '2001:4860:4860::8844']
resolver.timeout = 2
def can_resolve_google_com(resolver, nameserver):
    try:
        resolver.nameservers = [nameserver]
        result = resolver.resolve("google.com", "A")
        return result is not None
    except dns.resolver.NoAnswer:
        return False
    except dns.resolver.Timeout:
        return False

def display_menu():
    print("Commands Menu")
    print("1. Find IP from DNS")
    print("2. Find DNS from IP")
    print("3. Change DNS server")
    print("4. Display current NS")
    print("5. Exit")

def execute_command(choice):
    global resolver
    if choice == "1":
        while True:
            print("Enter the DNS (or type 'back' to return to the main menu):")
            user_input = input().strip()
            if user_input.lower() == 'back':
                break
            try:
                ip = resolver.resolve(user_input, 'A')
                for i in ip:
                    print(i.to_text())
            except (dns.resolver.NoAnswer, dns.resolver.NXDOMAIN):
                print("No IP found for this DNS or DNS does not exist!")
                break
    elif choice == "2":
        while True:
            print("Enter IP (or type 'back' to return to the main menu):")
            user_input = input().strip()
            if user_input.lower() == 'back':
                break
            try:
                found_dns = resolver.resolve(dns.reversename.from_address(user_input), 'PTR')
                for i in found_dns:
                    print(i.to_text())
            except dns.resolver.NoAnswer:
                print("No DNS found for this IP!")
                break
            except dns.exception.SyntaxError:
                print("Invalid IP address format! Please enter a valid IP address.")
                break
    elif choice == "3":
        while True:
            print("Enter new nameserver (IPv4 or IPv6) (or type 'back' to return to the main menu):")
            user_input = input().strip()
            if user_input.lower() == 'back':
                break
            if not can_resolve_google_com(resolver, user_input):
                print("Cannot resolve 'google.com' with this DNS server. Please try again with a different DNS server.")
                continue
            resolver.nameservers = [user_input]
            print("Nameserver changed!")
            break
    elif choice == "4":
        print("Current Nameservers:\n")
        print(resolver.nameservers)
    elif choice == "5":
        print("Exiting program...")

if __name__ == '__main__':
    while True:
        display_menu()
        choice = input("Enter your choice: ")
        execute_command(choice)
        if choice == "5":
            break
