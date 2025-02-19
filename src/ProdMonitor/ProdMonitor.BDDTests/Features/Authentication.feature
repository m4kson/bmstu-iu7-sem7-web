@auth
Feature: User Login with 2FA
  As a user, I want to log in using email and password, followed by a two-factor authentication code.
  
  Scenario: Successful login with correct credentials and 2FA code
    Given a registered user
    When the user enters valid credentials
    And the system sends a 2FA code to their email
    And the user enters the correct 2FA code
    Then the user should be successfully authenticated
    