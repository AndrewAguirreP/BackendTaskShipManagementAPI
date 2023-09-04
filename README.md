# Ship Management System

The Ship Management System is a software application designed to manage and track information related to ships, ports, and their statuses. This README provides an overview of the system, its features, instructions for installation and usage, and information about GitHub Actions for continuous integration.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Haversine Formula](#haversine-formula)
- [GitHub Actions](#github-actions)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Ship Management:**
  - Add, edit, and delete ship records.
  - View ship details including ship status, type, and more.
  - Track ship destinations and statuses.

- **Port Management:**
  - Manage ports, including their names and geographic coordinates.

- **Status Tracking:**
  - Record and update ship statuses, including velocity, latitude, and longitude.
  - Find the closest port to a ship's current location.

## Prerequisites

Before you begin, ensure you have met the following requirements:

- .NET 6.0 SDK
- Entity Framework Core
- SQL Server or another supported database (for data storage)

## Installation

1. Clone this repository to your local machine:

   ```bash
   git clone my_url
