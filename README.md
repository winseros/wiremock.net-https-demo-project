# WireMock.Net HTTPS Example project

This project demonstrates WireMock.Net HTTPS usage differences on Windows and on Linux (or in Docker).

## How to use this project

1. Download
2. Run `dotnet dev-certs https --trust`, **if your OS supports it** (At the time I write this doc, Windows - does support; Linux - does not)
3. Run `dotnet test`

OR

1. Download
2. Run `docker build`

## How to make it work on Linix or in Docker?

1. Make the `localhost.conf` file of content:
   ```
   [ req ]
   default_bits       = 2048
   default_keyfile    = localhost.key
   distinguished_name = req_distinguished_name
   req_extensions     = req_ext
   x509_extensions    = v3_ca
    
   [ req_distinguished_name ]
   commonName         = Common Name (e.g. server FQDN or YOUR name)
    
   [ req_ext ]
   subjectAltName = @alt_names
    
   [ v3_ca ]
   subjectAltName = @alt_names
   basicConstraints = critical, CA:false
   keyUsage = keyCertSign, cRLSign, digitalSignature,keyEncipherment
   extendedKeyUsage = 1.3.6.1.5.5.7.3.1
   1.3.6.1.4.1.311.84.1.1 = DER:01
    
   [ alt_names ]
   DNS.1   = localhost
   DNS.2   = 127.0.0.1
   ```
   Note the `1.3.6.1.4.1.311.84.1.1 = DER:01` it is critical for aspnet for [recognizing](https://github.com/dotnet/aspnetcore/blob/c75b3f7a2fb9fe21fd96c93c070fdfa88a2fbe97/src/Shared/CertificateGeneration/CertificateManager.cs#L81) the cert.
    
2. Generate the cert:
   ```
   openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout localhost.key -out localhost.crt -config localhost.conf -subj /CN=localhost
   openssl pkcs12 -export -out localhost.pfx -inkey localhost.key -in localhost.crt -passout pass:
   ```
3. Grab the `localhost.pfx` and `localhost.crt` and throw them into the target system. In case of `Docker` that would look:
   ``` dockerfile
   COPY localhost.crt /usr/local/share/ca-certificates/
   RUN dotnet dev-certs https --clean \
       && update-ca-certificates
   COPY localhost.pfx /root/.dotnet/corefx/cryptography/x509stores/my/
   ```
 4. Profit. The system has the aspnetcore dev cert trusted.
