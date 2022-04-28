package com.company;

/**
 * Pomocná výjimka pro neplatné vstupy
 */
public class InvalidInputException extends Exception {

    /**
     * Kontruktor pro třídu InvalidInputException
     *
     * @param message pomocná zpráva při nastání výjimky
     */
    public InvalidInputException(String message) {
        super(message);
    }
}
