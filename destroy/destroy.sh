gcloud config set project  gke-proj-1-394220
kubectl delete -f ../deployment/ingress.yaml
kubectl delete -f ../deployment/managed-cert.yaml
gcloud container clusters delete NAME $(gcloud container clusters list --location=europe-west1-b --format="value(nam
e)"| head -n 1) --location=europe-west1-b --quiet
