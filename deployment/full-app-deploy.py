import os
import subprocess
import time

# Define the deployment folders
deployment_folders = [
    "/home/ashishgoelhr/react-app/server-side/PizzaStore/deployment",
    "/home/ashishgoelhr/react-app/deployment",
    "/home/ashishgoelhr/react-app/frontend/pizza-menu/deployment",
]

# Loop through each folder and execute kubectl commands
for folder in deployment_folders:
    # Change directory to the current folder
    os.chdir(folder)

    # Execute kubectl apply command to deploy resources
    subprocess.run(["kubectl", "apply", "-f", "."])

    # Print confirmation message
    print(f"Kubernetes resources deployed from {folder}")

    # Wait for resource provisioning in the current folder
    if folder == "/home/ashishgoelhr/react-app/deployment":
        # Define the resource type to wait for (e.g., pods, deployments)
        resource_type = "pods"

        # Define the desired number of ready resources
        desired_ready_resources = 2

        # Wait for the desired number of resources to be ready
        while True:
            # Get the number of ready resources
            ready_resources = subprocess.check_output(
                ["kubectl", "get", resource_type, "-n", "default", "-o", "jsonpath='{.items[*].status.conditions[?(@.type==\"Ready\")].status}'"]
            ).decode("utf-8").count("True")

            # Check if the desired number of resources are ready
            if ready_resources >= desired_ready_resources:
                print(f"Resource provisioning in {folder} completed.")
                break

            # Wait for 5 seconds before checking again
            time.sleep(5)
