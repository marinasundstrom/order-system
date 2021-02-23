# Order System

Model of an Order System with Subscription support, with a simple Web UI for viewing the data that has been generated.

The architecture of the app is CQRS using MediatR. 

Web UI - Blazor server-side.

## Purpose

To test algorithm for generating Deliveries based on Subscription patterns.

## Concepts

The system is based on these concepts:

* Order and OrderItem
* Subscription and SubscriptionPlan
* Delivery and DeliveryItem

## How it works

***It is not (yet) possible to create any orders from the UI.***

You create an Order with one or multiple item(s). Then, you can either assign a Subscription to the entire Order, or to a specific OrderItem.
The generator will then generate Deliveries and Invoices from the Deliveries. (This is done at the start of the app)

A Subscription is based on a Subscription Plan which holds the pattern for generating dates when a Delivery should take place.

Other than just on Orders, you can also assign Delivery Addresses to OrderItems, which will turn them into their own Deliveries.

## Run the project

Requires the Tye global tools to be installed.

Run this command from the command line:

```
$ tye run
```

The SQL Server database is running in Docker.

The app is set to re-create the database, and seed it with data, everytime the app launches.

## Development requirements

* .NET 5 SDK
* Docker
* Visual Studio, any flavor (to edit code)
* Tye (global tools)
