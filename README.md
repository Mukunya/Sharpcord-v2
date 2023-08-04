# Sharpcord-v2

This project makes it easy to run many discord bots on a single port.

Supports interactions and webhooks for now.

## Using the module system

Sharpcord relies on a system of modules. Bots should be made as class libraries with a reference to the bot library.
The SharpcordHost is the software that runs all the bots places in the modules directory as dlls.

## Logging

The bot library has a logging system. Use the Logger static class.
The sender should always be the source of the log message, as that specifies the prefix of the logged message if the LogContext attribute is present. For static sources, the sender can be null.
