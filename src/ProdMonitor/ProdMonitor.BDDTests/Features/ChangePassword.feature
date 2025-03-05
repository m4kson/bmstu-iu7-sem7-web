@change-password
Feature: User change his password with 2FA
    As a user, I want to change my password using my current password, followed by a two-factor authentication code.
    
    Scenario: Successful password change with correct credentials and 2FA code
        Given a registered user
        When the user enters valid credentials
        And the system sends a 2FA code to their email
        And the user enters the correct 2FA code
        And the user enters a new password
        And the system sends a 2FA code to their email
        And the user enters the correct 2FA code
        Then the user should be able to log in with the new password