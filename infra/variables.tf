variable "project_id" {
  description = "The GCP project ID"
  type        = string
}

variable "region" {
  description = "The GCP region"
  type        = string
  default     = "europe-west2"
}

variable "zone" {
  description = "The GCP zone"
  type        = string
  default     = "europe-west2-a"
}

variable "cluster_name" {
  description = "The name of the GKE cluster"
  type        = string
}

variable "node_count" {
  description = "The number of nodes in the cluster"
  type        = number
  default     = 1
}

variable "machine_type" {
  description = "The machine type for the cluster nodes"
  type        = string
  default     = "e2-medium"
}
