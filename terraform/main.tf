data "aws_vpc" "default" {
  default = true
}

data "aws_subnets" "default" {
  filter {
    name   = "vpc-id"
    values = [data.aws_vpc.default.id]
  }
}

resource "aws_key_pair" "eagletech_key" {
  key_name   = "eagletech-key-${formatdate("YYYYMMDDHHmmss", timestamp())}"
  public_key = file("~/.ssh/id_rsa_terraform.pub")
}

resource "aws_security_group" "sg-eagletech" {
  vpc_id = data.aws_vpc.default.id

  ingress {
    description = "SSH"
    from_port   = 22
    to_port     = 22
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "HTTP"
    from_port   = 80
    to_port     = 80
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "HTTPS"
    from_port   = 443
    to_port     = 443
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  ingress {
    description = "API"
    from_port   = 5000
    to_port     = 5000
    protocol    = "tcp"
    cidr_blocks = ["0.0.0.0/0"]
  }

  egress {
    from_port   = 0
    to_port     = 0
    protocol    = "-1"
    cidr_blocks = ["0.0.0.0/0"]
  }

  tags = {
    Name = "sg-eagletech"
  }
}

resource "aws_instance" "instancia" {
  ami                    = "ami-0360c520857e3138f"
  instance_type          = "t3.micro"
  key_name               = aws_key_pair.eagletech_key.key_name
  vpc_security_group_ids = [aws_security_group.sg-eagletech.id]

  tags = {
    Name = "UbuntuServer"
  }
}
